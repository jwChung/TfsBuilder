##Visual Studio Online Service Hook##

### Overview ###
This project is a service hook to queue a build process to [Visual Studio Online] and is serviced on
<http://tfsbuilder.apphb.com/>.

### How to queue a build process on GitHub ###
1. Open the Service Hooks page of a project on Github.(Settings > Service Hooks)
2. On the left pane of the page, select the WebHook URLs service.
3. Add a url as the following form.

        https://tfsbuilder.apphb.com/api/{0}/{1}/{2}/?username={3}&password={4}

        where,
        {0}: An account name of visual studio online,
             which is the '{0}' part of https://{0}.visualstudio.com
        {1}: An project name in the account,
             which is the '{1}' part of https://{0}.visualstudio.com/DefaultCollection/{1}
        {2}: An build definition name of the project, which you need to create on the online.
        {3}: Your credential name on the online.
        {4}: Your credential password on the online.

### Caution ###

1. Your name and password will be secured with using HTTPS(SSL) even if these are sent as query string of the end of the
   url. However, we recommend to use ALTERNATE AUTHENTICATION CREDENTIALS. ([Learn more])
2. Use the '/' character before the '?' mark to send query string
   (Do not use as https://tfsbuilder.apphb.com/api/{0}/{1}/{2}?...),
   which can cause TfsBuilder not to queue your build process to Visual Studio Online.

### How to test ###
If you have any problem on queuing a build process, you can check out the problem with a test html file.
First, make a html file having the following content, open the file with a web browser, click the button 'Queue build',
and then check out a message on the web page.

    <html>
        <head>
            <title>Queue build tester</title>
        </head>
        <body>
            <form action="https://tfsbuilder.apphb.com/api/{0}/{1}/{2}/" method="POST">
                <input type="hidden" name="username" value="{3}" />
                <input type="hidden" name="password" value="{4}" />
                <input type="hidden" name="payload" value="dummy" />
                <input type="submit" value="Queue build" />	
            </form>
            <!--
                where,
                {0}: An account name of visual studio online,
                     which is the '{0}' part of https://{0}.visualstudio.com
                {1}: An project name in the account,
                     which is the '{1}' part of https://{0}.visualstudio.com/DefaultCollection/{1}
                {2}: An build definition name of the project, which you need to create on the online.
                {3}: Your credential name on the online.
                {4}: Your credential password on the online.
            -->
        </body>
    </html>

[Visual Studio Online]: http://www.visualstudio.com
[Learn more]: http://www.visualstudio.com/en-us/get-started/share-your-xcode-projects-vs