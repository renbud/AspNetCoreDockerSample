This project was created as a proof of concept for a few pieces of DQP Portal project

. Use react & node.js with Microsoft stack
. Deploy to docker containers
. Access sharepoint libraries from the containers (No .Net framework libraries)


# Node and MVC in .Net Core
These steps
. Use Visual Studio 2019 version 16.3.9 (or later)
. Select template for new project (ASPNET Core Web Application)
         -> Choose .Net Core 3.0
		 -> Choose React.js

(I found that Node.js stays running, holding locks, even after I have stopped debugging and existed Visual Studio
You can see it in Task Manager & Resmon)



# Docker
Right mouse on project & select "Add docker support". This adds a dockerfile to the project
This default dockerfile does not include node/NPM which is required for this project
	For linux its easy: to add the following 2 lines to run on build)
		RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
		RUN apt-get install -y nodejs

	If we use windows nanoserver: then https://blogs.technet.microsoft.com/nanoserver/2016/05/04/node-js-on-nano-server/

To build the docker image:
	>docker build -t netcoreexample .

To run the docker image in a new container:
	>docker run -d -p 8080:80 --name example netcoreexample


# Sharepoint
Getting sharepoint to work with .Net Core is a challenge.
The way I got this to work uses the CSOM (client side object model)
The downside is that this code must run on Windows. (It can be a console app or a web service and can be dockerised, but must be Windows OS)
I am trying to set up the dockerfile for windows nanoserver.
The reason is that although CSOM says it is "portable" that does not include Linux.
 * You need to download the sharepoint portable DLLs (NuGet does not work for portable!)
 * https://www.microsoft.com/en-us/download/details.aspx?id=42038
 * When the installer runs, the DLLs live here on a windows box in the following path C:\Program Files\Common Files\microsoft shared\Web Server Extensions\16\ISAPI\
 * You need to reference
 * Microsoft.Sharepoint.Client.Portable
 * Microsoft.Sharepoint.Client.Runtime.Portable
 * Microsoft.Sharepoint.Client.Runtime.Windows
 *  The last one (Runtime.Windows) needs to be referenced by the final executable - not a library project - as it is loaded at runtime
 *  I stick that DLL in a subfolder to get the docker build to work
 * 
 * See this article:
 * https://social.msdn.microsoft.com/Forums/office/en-US/3dacdc8f-a819-4451-8b2c-10f8f14e832b/sharepoint-online-client-components-sdk-does-not-work-with-net-core-20

