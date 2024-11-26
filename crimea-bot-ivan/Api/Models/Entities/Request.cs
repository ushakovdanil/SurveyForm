using Api.Models.Entities.Abstract;
using Api.Models.Enums;
using System.Reflection;

namespace Api.Models.Entities
{
    public class Request : BaseEntity
    {
        public UserEntity User { get; set; }
        public string? Сitizenship { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRequestStatus Status { get; set; }
        public UserRequestRating Rating { get; set; }
        public bool Criminal { get; set; }
        public bool ExpungedCriminal { get; set; }
        public bool Military { get; set; }
        public string? Age { get; set; }

        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(Request);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(Request);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }
        public Request() { }
        public Request(UserEntity user,
            string сitizenship, 
            string fullName, 
            string phoneNumber,
            bool criminal,
            bool expungedCriminal,
            bool military,
            string age) : base() 
        {
            User = user;
            Сitizenship = сitizenship;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Criminal = criminal;
            ExpungedCriminal = expungedCriminal;
            Military = military;
            Age = age;
            Status = UserRequestStatus.InProgres;
            Rating = UserRequestRating.Green;
        }

        public void CalculateRating()
        {
            if (Criminal && !ExpungedCriminal)
            {
                Status = UserRequestStatus.Refused;
                return;
            }

            int age = Convert.ToInt32(Age);

            if (!Criminal && age >= 20 && age <= 35 && !Military)
            {
                Rating = UserRequestRating.Green;
            }
            else if(ExpungedCriminal && age > 35 && age < 50 && Military)
            {
                Rating = UserRequestRating.Yellow;
            }
            else if(ExpungedCriminal && age >= 50 && Military)
            {
                Rating = UserRequestRating.Red;
            }
            else
            {
                Rating = UserRequestRating.Gray;
            }

            Status = UserRequestStatus.Done;
        }
    }
}
