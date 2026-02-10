# 📚 Smart Library – Digital Library Management System

Smart Library is a **Digital Library Management System** built with a scalable **ASP.NET Core Web API** backend and consumed by both **Web (Next.js)** and **Mobile (Flutter)** applications.

The project focuses on **clean architecture**, **secure authentication**, and **real-world business logic** for managing books, users, borrowing, and favorites.

---

## 👨‍💻 My Role
- **Backend Developer & Team Lead**
- Responsible for:
  - Backend architecture design
  - API development
  - Authentication & authorization
  - Business logic implementation
  - Integration with frontend & mobile apps

---

## 🧱 Backend Architecture
- **ASP.NET Core Web API**
- **Layered Architecture**
  - Controllers
  - Services
  - Repositories
  - DTOs
- **Unit of Work Pattern**
- **AutoMapper**
- **JWT Authentication & Role-Based Authorization**
- **Clean separation of concerns**

---

## 🔐 Authentication & Authorization
- JWT-based authentication
- Role-based authorization:
  - **Admin**
  - **User**
- Secure endpoints using `[Authorize]` & `[Authorize(Roles = "Admin")]`

---

## 📚 Core Features

### 👤 Account Management
- Register & Login
- Role assignment (Admin only)

### 📖 Books Management (Admin)
- Create, update, delete & view books
- Upload book images
- Assign categories
- Search books by title
- Recommended books endpoint

### 🗂 Categories
- Full CRUD operations

### 🔁 Borrowing System
- Borrow & return books
- Track available copies
- Prevent duplicate active borrows
- Borrow history per user

### ❤️ Favorites
- Add / remove books from favorites
- User-specific favorite list

### 👤 User Profile
- View personal information
- Borrow history
- Favorite books

## 🛠 Technologies Used

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication

### Frontend
- Next.js
- TypeScript
- Tailwind CSS

### Mobile
- Flutter
- Cubit (Bloc)
- GetIt
- Shared Preferences

---

## 🧪 API Documentation
- Swagger UI used for API testing and documentation  
- Includes all endpoints for authentication, books, borrows, favorites, and users

---

## 🤝 Team Collaboration
This project was built as a **team-based project**, focusing on:
- Real-world backend development
- API consumption from multiple clients
- Clean code & maintainability
- Effective communication between backend, frontend & mobile teams

---

## 📌 Future Enhancements
- Pagination & filtering
- Advanced recommendation system
- Notifications for return dates
- Admin dashboard analytics

---

## ⭐ Acknowledgment
Thanks to the frontend and mobile team members for the great collaboration on this project.

---

## 📬 Contact
If you have any questions or suggestions, feel free to reach out.

---
