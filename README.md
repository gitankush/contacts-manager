# ContactsManager

ContactsManager is a Web App for maintaining basic contact information:
- First Name
- Last Name
- Email
- Phone Number
- Status (Active/Inactive)
    
# Features
  - List contacts
  - Add a contact
  - Edit contact
  - Delete/Inactivate a contact 

# Project Structure

ContactsManager.sln

├── Evolent.ContactsMgmt.BL              `# Contains ContactRepository for performing CRUD operations in database. `

├── Evolent.ContactsMgmt.Common          `# For keeping common code across projects (such as contracts & helper classes)`

├── Evolent.ContactsMgmt.DataSource      `# Data Access project containing edmx file generated from ContactManagerDB` 

├── Evolent.ContactsMgmt.DTOs            `# Data Transfer Objects - Model classes used in WebApi for data transfer`   

├── Evolent.ContactsMgmt.WebApi          `# Contains Contact Manager Rest APIs`

├── Evolent.ContactsMgmt.WebApi.Tests    `# Contains test methods for WebAPI methods.`

├── Evolent.ContactsMgmt.WebApp          `# UI Layer for ContactsManager.`

├── Evolent.ContactsMgmt.WebAppTests     `# Contains test methods for WebApp Controller Methods`

└── README.md

# Getting Started

- Prerequisties
  - Microsoft SQL Server or SQLExpress Edition
  - Visual Studio 2017 or above
  - IIS 7

- Installation

  - Database Setup:
  
    1. Open SQL Server Management Studio and connect to MSSQL/SQLExpress server where you want to create the ContactManager database.
    
    2. After connecting to the server, create database with the name `ContactsManagerDB`.
    
    3. Execute below database script to create Contact table:
    
      ```sql

      USE [ContactsManagerDB]
      GO

      /****** Object:  Table [dbo].[Contact]    Script Date: 8/22/2019 9:33:36 AM ******/
      SET ANSI_NULLS ON
      GO

      SET QUOTED_IDENTIFIER ON
      GO

      SET ANSI_PADDING ON
      GO

      CREATE TABLE [dbo].[Contact](
        [ContactID] [int] IDENTITY(1,1) NOT NULL,
        [FirstName] [varchar](50) NOT NULL,
        [LastName] [varchar](50) NOT NULL,
        [Email] [varchar](100) NOT NULL,
        [PhoneNumber] [varchar](50) NOT NULL,
        [Status] [bit] NOT NULL,
       CONSTRAINT [PK_Contact_ID] PRIMARY KEY CLUSTERED 
      (
        [ContactID] ASC
      )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
      ) ON [PRIMARY]

      GO

      SET ANSI_PADDING OFF
      GO

      ```
     
     4. Add few contacts manually in Contact table for testing purpose.
     
    - Application Setup: Open ContactsManager.sln in VS2017 and follow below steps to setup the application:
    
     - Database Connection String:
      1. Modify `ContactsManagerDBEntities` connection string in `Web.config` files for both `Evolent.ContactsMgmt.WebApi` and             `Evolent.ContactsMgmt.WebApp` projects. Ensure `data source`,`User Id` and  `Password` are correct as per the database server configured earlier. 
     
     - Publish ContactsManager WebApi on IIS:
      1. Right click on Evolent.ContactsMgmt.WebApi and click on Publish. A custom profile is already created in the project for publishing the WebApi in wwwroot folder of IIS. Modify the settings if required.
      2. Open IIS Manager. Under Application pools, add a new AppPool called `ContactsManagerAppPool`.
      3. Configure a new Website named `ContactsManagerApi` and select `ContactsManagerAppPool` under AppPool. In `Physical Path`, set the wwwroot directory where ContactManager WebApi was published. Also specify one of the available ports on the machine `e.g. 9000` .
      4. Check the `Start Website Immediately` checkbox and click OK. ContactsManager WebAPI should be up and running now. 
      5. Open the browser and enter the URL `http://localhost:[port]/api/contact/getAllContacts`. You must see all the contacts you have added in the database during setup.
     
     
     - Publish ContactsManager MVC WebApp on IIS:
      1. Right click on Evolent.ContactsMgmt.WebApp and click on Publish. A custom profile is already created in the project for publishing the WebApp in wwwroot folder of IIS. Modify the settings if required.
      2. Open IIS Manager. Under Application pools, add a new AppPool called `ContactsManagerAppPool`.
      3. Configure a new Website named `ContactsManagerWebApp` and select `ContactsManagerAppPool` under AppPool. In `Physical Path`, set the wwwroot directory where ContactManager WebApp was published. Also specify one of the available ports on the machine `e.g. 9001` .
      4. Check the `Start Website Immediately` checkbox and click OK. ContactsManager WebAPI should be up and running now. 
      5. Open the browser and enter the URL `http://localhost:[port]/`. You must see the All Contacts list page.
     
                             `ContactsManager setup is now completed. Thank you for installing it.`


# Author
  - Ankush Mahajan
   
   -`LinkedIn` https://in.linkedin.com/in/ankushmahajan
   
   -`Email` er.mahankush@gmail.com





