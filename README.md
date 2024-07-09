FileHandler is an ASP .NET core MVC web data visualization platform. It handles Text (.txt) and Excel (.xlsx) files using async post requests (threading), reading its contents and displaying them on the website. 

The contents are sent using an AJAX request to the HomeController, where the file is processed, read and saved to the server. After being returned, the data is rendered in the view for the client using JQuery tables, showing the data in a structured format.

The data visualization platform is secured by a login system, that connects to a SQL database storing the user's email and password where CRUD operations are implemented.

In the new version, there is cookie-based authentication and user claims implemented through a login page, where the user data is stored in a database using SQL and Azure data studio. The login page is made using react, html/css and bootstrap
