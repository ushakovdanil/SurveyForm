FROM php:8.1-cli

# installing cron package
RUN apt-get update

# installing PHP PDO extension to talk to MySQL
RUN apt-get install -y libpq-dev \
    && docker-php-ext-configure pgsql -with-pgsql=/usr/local/pgsql \
    && docker-php-ext-install pdo pdo_pgsql pgsql \
    && apt-get install -y systemd \
    && apt-get install -y nano \
    && apt-get -y install cron

# putting our test PHP script somewhere in the filesystem
RUN mkdir /cron_scripts
WORKDIR /cron_scripts
COPY Supervisor/cron.php /cron_scripts
COPY Supervisor/env.sh /cron_scripts

RUN systemctl enable cron

RUN (crontab -l -u root; echo "* * * * * /bin/bash /cron_scripts/env.sh; . /etc/environment; /usr/local/bin/php /cron_scripts/cron.php >> /cron_scripts/cron.log") | crontab
# creating the log file that will be written to at each cron iteration
RUN touch cron.log

CMD cron && tail -f /cron_scripts/cron.log


