namespace Apostle
{
    /// <summary>
    /// This class uses the type-safe-enum pattern to restrict the available options when setting
    /// the delivery host for apostle.io. The goal was to prevent an api client from setting an
    /// invalid url like http://yahoo.com. The Mail object will call Aspostle.Validate() before
    /// sending mail so in the event the Apostle.DeliveryHost was set to null by the api client
    /// it will throw an exception. Otherwise this should be sufficient to ensure valid values
    /// </summary>
    public sealed class DeliveryHosts
    {
        public static readonly DeliveryHosts Apostle = new DeliveryHosts("Apostle", @"http://deliver.apostle.io");

        private readonly string _name;
        private readonly string _value;

        private DeliveryHosts(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }
}