namespace Services.Util
{
    using System;

    public abstract class Preconditions
    {
        private const string DEFAULT_MESSAGE = "Received an invalid parameter";

        /**
         * Checks that an object is not null.
         *
         * @param obj any object
         * @param errorMsg error message
         *
         * @throws ArgumentException if the object is null
         */
        public static void CheckNotNull(object obj, string errorMsg)
        {
            Check(obj != null, errorMsg);
        }

        /**
         * Checks that a string is not null or empty
         *
         * @param str any string
         * @param errorMsg error message
         *
         * @throws ArgumentException if the string is null or empty
         */
        public static void CheckEmptyString(string str, string errorMsg)
        {
            Check(!string.IsNullOrWhiteSpace(str), errorMsg);
        }

        private static void Check(bool requirement, string error)
        {
            if (!requirement)
            {
                throw new ArgumentException(!string.IsNullOrWhiteSpace(error) ? error : DEFAULT_MESSAGE);
            }
        }
    }
}
