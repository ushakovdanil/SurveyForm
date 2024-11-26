<?php
ini_set('date.timezone', 'Europe/Kiev');
date_default_timezone_set('Europe/Kiev');

function sendToTelegram($token, $method, $response, $use_proxy = 0)
{
    //print_r($response);
    $ch = curl_init('https://api.telegram.org/bot' . $token . '/' . $method);
    curl_setopt($ch, CURLOPT_POST, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    curl_setopt($ch, CURLOPT_POSTFIELDS, $response);
    curl_setopt($ch, CURLOPT_HEADER, false);
    $res = curl_exec($ch);
    curl_close($ch);
    return $res;
}
function PDOPgSQL()
{// connect to DB PostgreSQL
    static $dbconn;
    if (is_null($dbconn)) {

        try {
            $host = $_ENV['APP_DB_HOST_NAME'];
            $port = 5432;
            $dbname = $_ENV['BOT_APP_DB_NAME'];
            $dsn = "pgsql:host=$host;port=$port;dbname=$dbname";
            $username = $_ENV['DB_BOT_APP_USER'];
            $passwd = $_ENV['DB_BOT_APP_PASSWORD'];
            $dbconn = new PDO($dsn, $username, $passwd);
            $dbconn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        } catch (PDOException $e) {
            print "Error!: " . $e->getMessage() . "<br />";
        }
    }

    return $dbconn;
}

function error($text)
{
    print $text;
}

function cron_init()
{
    $pdo = PDOPgSQL();
    $sql = 'SELECT "Requests".*,"Requests"."CreatedOn" AS "RequestCreatedOn","Requests"."Id" AS "RequestId","Users".* FROM "Requests","Users" WHERE "Requests"."Status" IN (1,3) AND "Users"."Id" = "Requests"."UserId"';
    $statement = $pdo->prepare($sql);
    $statement->execute();
    $Requests = $statement->fetchAll(PDO::FETCH_ASSOC);

    $sql = 'SELECT * FROM "Languages"';
    $statement = $pdo->prepare($sql);
    $statement->execute();
    $Languages = $statement->fetchAll(PDO::FETCH_ASSOC);

    if (count($Requests) == 0)
        return error("Requests not found");

    $rating = [
        0 => ['emoji' => '🟢', 'text' => 'Зелений'],
        1 => ['emoji' => '🟡', 'text' => 'Жовтий'],
        2 => ['emoji' => '🔴', 'text' => 'Червоний'],
        3 => ['emoji' => '⚪️', 'text' => 'Сірий']
    ];
    $military = [0 => 'Ні', 1 => 'Так'];
    $criminal = [0 => 'Відсутня', 1 => 'Є'];

    foreach($Requests as $request)
    {
        $unix = strtotime($request['RequestCreatedOn']);
        $lang = array_search($request['LanguageId'], array_column($Languages, 'Id'));
        $text_request = "<b>Статус:</b> ".$rating[$request['Rating']]['emoji']." ".$rating[$request['Rating']]['text']."
---------------------------------
".($request['Username'] ? "👤 @".$request['Username']."
" : "")."🆔 ".$request['TelegramId']."
📅 ".date("Y-m-d H:i:s", $unix)."
---------------------------------
<b>Громадянство:</b> ".$request['Сitizenship']."
<b>ПІБ:</b> ".$request['FullName']."
<b>Вік:</b> ".$request['Age']."р
<b>Телефон:</b> ".$request['PhoneNumber']."
<b>Судимість:</b> ".$criminal[$request['Criminal']].(($request['ExpungedCriminal'] && $request['Criminal']) ? " (Погашена)" : "").((!$request['ExpungedCriminal'] && $request['Criminal']) ? " (Не погашена)" : "")."
<b>Військовий:</b> ".$military[$request['Military']]."
<b>Мова:</b> ".$Languages[$lang]['Name']."
";
        $message = array(
            'chat_id' => $_ENV['TG_GROUP_ID'],
            'text' => $text_request,
            'parse_mode' => "html"
        ); 
        $result = sendToTelegram($_ENV['TG_KEY'], 'sendMessage', $message);
        $json = json_decode($result, true);
        if ($json['ok'] == true)
            $result_status = 2;
        else
        {
            $result_status = 3;
            error("Request not sended");
        }
        $sql = 'UPDATE "Requests" SET "Status" = '.$result_status.' WHERE "Id" = \''.$request['RequestId']."'";
        $statement = $pdo->prepare($sql);
        $statement->execute();
        sleep(2);
    }
}

cron_init();

