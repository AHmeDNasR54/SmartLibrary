# 📚 Smart Library – Digital Library Management System

Smart Library is a **team-based Digital Library Management System** consisting of:
- **ASP.NET Core Web API (Backend)**
- **Next.js Web Application (Frontend)**
- **Flutter Mobile Application**

The project is designed with a **clean, scalable architecture** and focuses on real-world scenarios such as authentication, borrowing workflows, favorites, and multi-client API consumption.

---

## 🚀 Live Demo 
- 🌐 **Web App (Vercel):** https://book-borrowing-system.vercel.app/
---

## 👥 Team Project Overview
This repository represents the **full system**, built collaboratively by the team:
- Backend (ASP.NET Core Web API)
- Frontend (Next.js)
- Mobile App (Flutter)

Each layer is designed to work seamlessly with the same backend API.

---

## 🧠 Backend – ASP.NET Core Web API
The backend serves as the core of the system and provides secure, reusable APIs for both web and mobile clients.

### 🔧 Architecture & Patterns
- ASP.NET Core Web API
- Layered Architecture:
  - Controllers
  - Services
  - Repositories
  - DTOs
- Unit of Work Pattern
- AutoMapper
- Clean separation of concerns

### 🔐 Authentication & Authorization
- JWT-based authentication
- Role-based authorization:
  - **Admin**
  - **User**
- Secure endpoints using `[Authorize]` attributes

### 📚 Backend Features
- Account Management (Register / Login / Roles)
- Books Management (CRUD, images, search, recommendations)
- Categories Management
- Borrowing System:
  - Borrow & return workflow
  - Availability tracking
  - Borrow history
- Favorites System
- User Profile with history & favorites
- Swagger UI for API documentation and testing

---

## 🌐 Web Frontend
Built using modern web technologies to deliver a fast and responsive UI.

### 🛠 Tech Stack
- Next.js
- TypeScript
- Tailwind CSS
- React Icons

### ✨ Features
- Authentication & authorization
- Browse library & book details
- Wishlist (favorites)
- User profile integration
- Fully integrated with the backend API

---

## 📱 Mobile Application (Flutter)
The Flutter application consumes the same backend API and provides a smooth mobile experience.

### 📲 Features
- Onboarding screens
- Authentication (Login & Register)
- Home, Library & Wishlist
- User Profile (borrowed books & return dates)

### 🛠 Tech Stack
- Cubit (Bloc) for state management
- Shared Preferences for local data storage
- GetIt for dependency injection

---

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
- Swagger UI is used to document and test all API endpoints
- Covers authentication, books, borrowing, favorites, and users

---

## 🤝 Team Collaboration
This project was developed as a **collaborative team effort**, focusing on:
- Clean architecture
- Reusable APIs
- Multi-client integration (Web & Mobile)
- Real-world development practices

---

## 🚧 Future Enhancements
- Pagination & filtering
- Advanced recommendation system
- Notifications for book return dates
- Admin dashboard analytics

---

## 📬 Contact
For any questions, suggestions, or collaboration opportunities, feel free to reach out.

---
