# HomeOwners

> **Team project.** See the [contributors](https://github.com/ianjohndal5/HomeOwners/graphs/contributors).

A homeowners' association portal built with ASP.NET Core MVC. Residents sign in, look up shared facilities and check their bills. It also has pages for booking, reservations, events, announcements and a community forum.

This is the first version of the project. The team later rebuilt it in Blazor as [LendSpace](https://github.com/ianjohndal5/LendSpace), which adds staff and admin roles, a reservation approval workflow, reviews and notifications.

## What's in it

| Area | What it does |
| --- | --- |
| Landing, sign-up, log-in | ASP.NET Core Identity with `Admin` and `User` roles and cookie authentication |
| Facilities | Lists facilities from the database, with search by name |
| Billing | Shows the signed-in resident's bills and whether each one is paid |
| Booking, reservations, events, announcements, forum, profile | Front-end pages only; not yet connected to the database |

## Built with

- .NET 9 and ASP.NET Core MVC with Razor views
- Entity Framework Core with SQLite
- ASP.NET Core Identity
- Tailwind CSS v4

## Running it locally

You need the [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0).

```bash
git clone https://github.com/ianjohndal5/HomeOwners.git
cd HomeOwners
dotnet run
```

Then open http://localhost:5242.

The SQLite database (`database.db`) is committed with sample data, so there is nothing to set up. Run the app from the repository root, because it looks for `database.db` in the current directory.

To try it, log in as the demo resident `user@email.com` with the password `password`. An `admin@email.com` account is also seeded, but this version has no admin pages.

## Project layout

```
Controllers/   Account (log-in, sign-up), User (resident pages), Home (landing)
Models/        Billing, Event, Facility, plus the Identity user and role
Data/          EF Core DbContext and seed data
Views/         Razor views for the landing page, auth, resident pages and shared navbars
Migrations/    EF Core migrations
wwwroot/       Tailwind build, CSS and images
```
