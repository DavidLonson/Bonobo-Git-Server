using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bonobo.Git.Server.Data.Update
{
    public class InitialCreateScript : IUpdateScript
    {
        public string Command
        {
            get 
            {
                return @"

                    CREATE TABLE IF NOT EXISTS [Repository] (
                        [Name] nvarchar(255) Not Null,
                        [DisplayName] nvarchar(200) Not null,
                        [Description] nvarchar(255) Null,
                        [Anonymous] Bit Not Null,
                        Constraint [PK_Repository] Primary Key ([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [Role] (
                        [Name] nvarchar(255) Not Null,
                        [Description] nvarchar(255) Null,
                        Constraint [PK_Role] Primary Key ([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [Team] (
                        [Name] nvarchar(255) Not Null,
                        [Description] nvarchar(255) Null,
                        Constraint [PK_Team] Primary Key ([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [User] (
                        [Name] nvarchar(255) Not Null,
                        [Surname] nvarchar(255) Not Null,
                        [Username] nvarchar(255) Not Null,
                        [Password] nvarchar(255) Not Null,
                        [Email] nvarchar(255) Not Null,
                        Constraint [PK_User] Primary Key ([Username])
                    );

                    CREATE TABLE IF NOT EXISTS [TeamRepository_Permission] (
                        [Team_Name] nvarchar(255) Not Null,
                        [Repository_Name] nvarchar(255) Not Null,
                        Constraint [UNQ_TeamRepository_Permission_1] Unique ([Team_Name], [Repository_Name]),
                        Foreign Key ([Team_Name]) References [Team]([Name]),
                        Foreign Key ([Repository_Name]) References [Repository]([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [UserRepository_Administrator] (
                        [User_Username] nvarchar(255) Not Null,
                        [Repository_Name] nvarchar(255) Not Null,
                        Constraint [UNQ_UserRepository_Administrator_1] Unique ([User_Username], [Repository_Name]),
                        Foreign Key ([User_Username]) References [User]([Username]),
                        Foreign Key ([Repository_Name]) References [Repository]([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [UserRepository_Permission] (
                        [User_Username] nvarchar(255) Not Null,
                        [Repository_Name] nvarchar(255) Not Null,
                        Constraint [UNQ_UserRepository_Permission_1] Unique ([User_Username], [Repository_Name]),
                        Foreign Key ([User_Username]) References [User]([Username]),
                        Foreign Key ([Repository_Name]) References [Repository]([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [UserRole_InRole] (
                        [User_Username] nvarchar(255) Not Null,
                        [Role_Name] nvarchar(255) Not Null,
                        Constraint [UNQ_UserRole_InRole_1] Unique ([User_Username], [Role_Name]),
                        Foreign Key ([User_Username]) References [User]([Username]),
                        Foreign Key ([Role_Name]) References [Role]([Name])
                    );

                    CREATE TABLE IF NOT EXISTS [UserTeam_Member] (
                        [User_Username] nvarchar(255) Not Null,
                        [Team_Name] nvarchar(255) Not Null,
                        Constraint [UNQ_UserTeam_Member_1] Unique ([User_Username], [Team_Name]),
                        Foreign Key ([User_Username]) References [User]([Username]),
                        Foreign Key ([Team_Name]) References [Team]([Name])
                    );
                  
                    ";
            }
        }

        public string Precondition
        {
            get { return null; }
        }
    }
}