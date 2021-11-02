using System;

namespace UI.ViewModel
{
    class ViewModelExceptions : Exception
    {

    }
    public class InputIsEmptyexception : Exception
    {
        public InputIsEmptyexception(string message) : base(message)
        {

        }
    }
}
