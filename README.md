# Bloggy API
Bloggy is a comprehensive API for managing blog posts, comments, and user interactions. It is designed to support dynamic content creation and engagement through features such as post creation, comment management, and user authentication. This project offers a modern approach for creating and managing blogs with secure user authentication and role-based access control.

## Technologies Used
* ASP.NET Core 8
* Entity Framework Core
* RESTful APIs
* Identity for User Management
* JWT Authentication
* AutoMapper
* MS SQL Server

## Architecture
Bloggy follows a modular, N-tier architecture, which includes:

* **Business Layer**: Implements the core logic for handling posts, comments, and user actions.
* **Data Access Layer**: Uses the Repository pattern for efficient interaction with the database.
* **Presentation Layer**: Exposes the API endpoints and manages user interactions.
* **Repository Pattern**: Organizes data retrieval and persistence logic.
* **Dependency Injection**: Ensures the codebase remains modular, testable, and maintainable.

### User Management and Authentication
* User registration and login.
* Password reset.
* Users Roles
* Manage user profile details.
* Role-Based Access Control (RBAC) for secure access to features.
* Manage CORS

### Post Management
* Create, update, and delete posts.
* **Pagination** for fetching posts efficiently, with options for setting the page size and page number.
* Search and filter posts by title, content, or creator.

### Comment Management
* Add, update, and delete comments on posts.
* Filter comments for the user.

### Favorites and Likes
* Add and remove posts from favorites.

## Running the Project
To run Bloggy API:

1. Clone the repository.
2. Add the `appsettings.json` file.
3. Configure the connection string in `appsettings.json` for database interaction.
4. Run database migrations to set up the database structure.
5. Build and run the application using Visual Studio or the .NET CLI.

## Configuration
Ensure that your `appsettings.json` is properly configured for your local environment. Here is a basic example:

```json
{
    "ConnectionStrings": {
    "DefaultConnection": "YourConnectionString"
    },
    "Jwt": {
    "Key": "Your32CharactersKey",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience",
    "LifeTime": 90 // Token lifetime in minutes
    }
}

## Contact
For any inquiries or issues, please contact the repository owner @MoustafaGamal01.