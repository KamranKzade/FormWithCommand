using FormWithCommand.Models;
using System.Collections.Generic;

namespace FormWithCommand.Repository;

public class FakeRepo
{
    public static List<Human> GetHumen()
    {
        return new List<Human>
        {   new Human
            {
                FirstName= "Kamran",
                LastName= "Karimzada",
                PhoneNumber="+994 70 370 20 16",
                EmailAddress= "311_kamran@mail.ru"
            },
            new Human
            {
                FirstName= "Aydin",
                LastName= "Hasimli",
                PhoneNumber="+994 70 852 94 10",
                EmailAddress= "aydin@gmail.com"
            },
            new Human
            {
                FirstName= "Elvin",
                LastName= "Camalzada",
                PhoneNumber="+994 51 584 87 62",
                EmailAddress= "elvin@yahoo.com"
            }
        };
    }

}
