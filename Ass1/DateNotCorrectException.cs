using System;
namespace Ass1
{
    public class DateNotCorrectException :Exception
    {
        public DateNotCorrectException()

        {
        }

        public DateNotCorrectException(string message):base (message)
        {

        }


    }
}
