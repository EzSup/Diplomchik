![image](https://github.com/user-attachments/assets/6ed72f6e-0d46-4fe5-85cd-f3853b2e5162)### Українська версія (English version below)

# Веб-сервіс ресторану

## Опис

Цей проєкт являє собою систему управління рестораном, що складається з **API** та **клієнтської частини** (front-end). Він дозволяє керувати користувачами, стравами, замовленнями та іншими аспектами ресторану. Проєкт реалізований з використанням **ASP.NET Core** для бекенду та **Blazor** з бібліотекою компонентів **MudBlazor** для фронтенду.
Проект знаходиться на хостингу за [цим посиланням](https://a4itxd.realhost-free.net/)

Репозиторій містить дві основні гілки:
- **APIFinal** – фінальна версія бекенду.
- **ClientFinal** – фінальна версія фронтенду.

---

## Технології

### Бекенд (гілка APIFinal)

- **ASP.NET Core** – серверна платформа для побудови API.
- **Entity Framework Core** – ORM для роботи з базою даних PostgreSQL.
- **PostgreSQL** – база даних для зберігання інформації.
- **JWT аутентифікація** – для забезпечення безпеки та керування ролями.
- **Swagger** – для документування та тестування RESTful API.

### Фронтенд (гілка ClientFinal)

- **Blazor** – фронтенд фреймворк для побудови динамічного інтерфейсу.
- **MudBlazor** – бібліотека компонентів для стилізації інтерфейсу.

### Сторонні сервіси

- **Cloudinary** - для розміщення зображень в хмарі.

---

## Функціонал

### Для користувачів (не авторизованих)
- Можливість перегляду списку страв, блогів, відгуків, вільних столиків ресторану
  ![image](https://github.com/user-attachments/assets/ca786946-1ba8-474c-90da-16b49e7db38b)

### Для користувачів (авторизованих)
- Можливість здійснення замовлень із резервацією столика
- Замовлення страв із доставкою
- Збереження усіх чеків у кабінеті користувача
- Можливість відкладеної оплати чеку
![image](https://github.com/user-attachments/assets/bf13de9f-3c3a-44ad-831b-e0ec7031c496)

### Для адміністраторів
- Адміністративна панель для керування контентом ресторану.
- Інтерактивні форми для створення та редагування страв та замовлень.
- Інтеграція з API для відображення та зміни даних в реальному часі.
![image](https://github.com/user-attachments/assets/da9827d4-1cf0-48ed-8801-7ab222f628ae)

---

### English version

# Restaurant web-service

## Description

This project is a restaurant management system consisting of **API** and an **client side** (front-end). It allows managing users, dishes, orders, and other aspects of the restaurant. The project is built using **ASP.NET Core** for the backend and **Blazor** with the **MudBlazor** component library for the frontend.
The project is hosted and available by [this link](https://a4itxd.realhost-free.net/)

The repository contains two main branches:
- **APIFinal** – final backend version.
- **ClientFinal** – final frontend version.

---

## Technologies

### Backend (APIFinal branch)

- **ASP.NET Core** – server-side platform for building APIs.
- **Entity Framework Core** – ORM for working with PostgreSQL.
- **PostgreSQL** – database for storing information.
- **JWT authentication** – for security and role management.
- **Swagger** – for documenting and testing RESTful APIs.

### Frontend (ClientFinal branch)

- **Blazor** – frontend framework for building dynamic user interfaces.
- **MudBlazor** – component library for UI styling.

### Side services

- **Cloudinary** - hosting images in cloud.
---

## Functionality

### For Users (Non-Authorized)
- Ability to view the list of dishes, blogs, reviews, and available restaurant tables
  ![image](https://github.com/user-attachments/assets/ca786946-1ba8-474c-90da-16b49e7db38b)

### For Users (Authorized)
- Ability to place orders with table reservation
- Ordering dishes with delivery
- Saving all receipts in the user's account
- Ability to postpone check payment
![image](https://github.com/user-attachments/assets/bf13de9f-3c3a-44ad-831b-e0ec7031c496)

### For Administrators
- Admin panel for managing restaurant content.
- Interactive forms for creating and editing dishes and orders.
- API integration for real-time data display and modification.
![image](https://github.com/user-attachments/assets/da9827d4-1cf0-48ed-8801-7ab222f628ae)


