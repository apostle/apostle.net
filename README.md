Apostle .Net
===========

.Net client for [Apostle.io](http://apostle.io/ "Apostle.io")

# Installation
To install Json.NET, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    Install-Package Apostle.Net
or you can search for Apostle.Net in the Nuget Package Manager UI.

# Configuration
Before you can send mail you have to configure your domain key with the Apostle.Net api client. This is done by setting the DomainKey property of the Apostle class as shown here:

	Apostle.DomainKey = "<your domain key>";

By default the delivery hostname is set to http://deliver.apostle.io. If you are using a different delivery host you can change it by setting the DeliveryHost property of the Apostle class as shown here:

	Apostle.DeliveryHost = DeliveryHosts.Apostle;

# Sending Mail
To send mail you need three things:

1. Domain Key
1. Template Name
1. Recipient Email Address

Once you are ready to send mail you can simply do the following:

	Apostle.DomainKey = "<your domain key>";
	var mail = new Mail("template name>", "<recipient email address>");
	mail.Deliver();

# Sending Mail With Data

# Sending Multiple Mails At Once

# Expected Exceptions
There are a number of exceptions that can happen when using the Apostle.Net api client that you should be aware of:
### Missing Delivery Hostname Exception
When you call `Mail.Deliver()` or `MailQueue.Deliver()` the Apostle.Net api client will verify that the delivery host has been set. If this property is null you will receive a `MissingDeliveryHostnameException`

### Missing Domain Key Exception
When you call `Mail.Deliver()` or `MailQueue.Deliver()` the Apostle.Net api client will verify that the domain key has been set. If this property is null or empty you will receive a `MissingDomainKeyException`

# Building This Solution
Before you run the tests in this solution you will need to provide your own Apostle Domain Key and the email address where you want to send the mail messages to. These are defined in the private.config file located in the Apostle.Net.Tests directory. You will need to rename private.config.template to private.config and then set the necessary values. 

The .gitignore is already setup to ignore the private.config file so you don't have to worry about your domain key and email address being stored in the git repo.

# Feedback

# Contributing
