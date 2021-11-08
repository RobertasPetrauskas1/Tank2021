using System;
using System.Collections.Generic;
using System.Text;

namespace Tank2021SharedContent.Facade
{
    public static class FacadeSingleton
    {
        static Facade facade;
        public static void setFacade(Facade facade)
        {
            FacadeSingleton.facade = facade;
        }
        public static Facade getFacade()
        {
            return facade;
        }
    }
}
