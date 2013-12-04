Apostle .Net
===========

.Net client for [Apostle.io](http://apostle.io/ "Apostle.io")

## Installation
To install Json.NET, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)
```
Install-Package Apostle.Net
```
or you can search for Apostle.Net in the Nuget Package Manager UI.

## Configuration
Before you can send mail you have to configure your domain key with the Apostle.Net api client. This is done by setting the DomainKey property of the Apostle class as shown here:
```csharp
Apostle.DomainKey = "<your domain key>";
```
By default the delivery hostname is set to http://deliver.apostle.io. If you are using a different delivery host you can change it by setting the DeliveryHost property of the Apostle class as shown here:
```csharp
Apostle.DeliveryHost = DeliveryHosts.Apostle;
```
## Sending Mail
To send mail you need a minimum of three things:

1. Domain Key
1. Template Name
1. Recipient Email Address

Once you are ready to send mail you can simply do the following:
```csharp
Apostle.DomainKey = "<your domain key>";
var mail = new Mail("template name>", "<recipient email address>");
var deliveryResults = mail.Deliver();
```

## Sending Mail With Data
If your mail template has replaceable tokens or if you want to override some of the default settings when sending mail you can do so by populating the optional properties of the Mail object as shown below.
```csharp
var mail = new Mail("welcome", "recipient@email.com");

mail.Name = "John Smith";
mail.From = "jsmith@email.com";
mail.Headers.Add("Test Header", "Test Header Value");
mail.Headers.Add("Test Header 2", "Test Header 2 Value");
mail.Headers.Add("Test Header 3", "Test Header 3 Value");
mail.LayoutId = "standard";
mail.ReplyTo = "support@email.com";
mail.Data.Add("Example Data", "Example data values. This can be of any type");
mail.Data.Add("Example Array Data", new[] { 1, 2, 3, 4 });
mail.Data.Add("items", new[] { new { Name = "Widget", Price = 4.00, Quantity = 2 }, new { Name = "Widget 2", Price = 5.00, Quantity = 1 }, new { Name = "Widget 3", Price = 33.00, Quantity = 13 } });

var deliveryResult = mail.Deliver();
```


## Sending Multiple Mails At Once
If you are sending multiple mails you can make the process more efficient by using the MailQueue. You can add as many Mail objects to the MailQueue as you'd like before delivering the mail. By using the MailQueue you will only make one request to [Apostle.io](http://apostle.io/ "Apostle.io") instead of making one request per Mail.
```csharp
var mailQueue = new MailQueue();

mailQueue.Add(new Mail("welcome", "recipient.1@email.com"));
mailQueue.Add(new Mail("welcome", "recipient.2@email.com"));
mailQueue.Add(new Mail("welcome", "recipient.3@email.com"));

var deliveryResult = mailQueue.Deliver();
```

## Verifying Results
Both the Mail and MailQueue return a DeliveryResult when you call their Deliver() methods. The DeliveryResult will contain the following properties that will help you verify that the Mail was accepted by  [Apostle.io](http://apostle.io/ "Apostle.io")

- **Status** - Enumeration that tells you the status of the call to the Apostle.io api (Queued or Error)
- **Message** - If the Status property is Error then this property will tell you why the error occurred.
- RequestData
- **JsonRequest** - This property will show you the json that was sent from your application to the Apostle.io api. This is useful for logging and/or debugging.
- **ApiResponse** - This property will show you the response that was received by your application from the Apostle.io api. This is useful for logging and/or debugging


## Expected Exceptions
There are a number of exceptions that can happen when using the Apostle.Net api client that you should be aware of:
### Missing Delivery Hostname Exception
When you call `Mail.Deliver()` or `MailQueue.Deliver()` the Apostle.Net api client will verify that the delivery host has been set. If this property is null you will receive a `MissingDeliveryHostnameException`

### Missing Domain Key Exception
When you call `Mail.Deliver()` or `MailQueue.Deliver()` the Apostle.Net api client will verify that the domain key has been set. If this property is null or empty you will receive a `MissingDomainKeyException`

## Building This Solution
Before you run the tests in this solution you will need to provide your own Apostle Domain Key and the email address where you want to send the mail messages to. These are defined in the private.config file located in the Apostle.Net.Tests directory. You will need to rename private.config.template to private.config and then set the necessary values. 

The .gitignore is already setup to ignore the private.config file so you don't have to worry about your domain key and email address being stored in the git repo.

## Feedback
If you find anything that needs attention please create an [issue](https://github.com/apostle/apostle.net/issues).

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create new Pull Request
