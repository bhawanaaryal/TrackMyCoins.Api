# Track My Coins 💰

Track My Coins is a full-stack web-based expense management system designed to help users efficiently manage their daily financial activities. The application allows users to record, categorize, and monitor expenses while providing meaningful insights into spending behavior through a clean and interactive dashboard.

---

## 🚀 Features

* Add, edit, and delete expenses
* Categorize transactions for better organization
* Set and manage budgets
* Dashboard with summarized financial insights
* User authentication using JWT
* Responsive and user-friendly interface

---

## 🛠️ Tech Stack

### Frontend

* Angular (Standalone Components)
* TypeScript
* HTML, CSS

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server

---

## 📁 Project Structure

```
TrackMyCoins
│
├── backend
│   └── TrackMyCoins.Api
│
└── frontend
```

---

## ⚙️ Getting Started

### 1. Clone the Repository

```
git clone https://github.com/your-username/track-my-coins.git
cd track-my-coins
```

---

### 2. Backend Setup

```
cd backend/TrackMyCoins.Api
dotnet restore
dotnet run
```

#### Requirements:

* .NET SDK installed
* SQL Server running

#### Configuration:

Update the `appsettings.json` file with your database connection string and JWT settings.

---

### 3. Frontend Setup

```
cd frontend
npm install
ng serve
```

Open your browser and navigate to:

```
http://localhost:4200
```

---

## 🔐 Configuration Notes

* Ensure the API base URL in Angular matches the backend URL
* Configure JWT secret key properly in backend
* Run database migrations if required

---


## 🤝 Contributing

Contributions are welcome. If you'd like to improve the project:

1. Fork the repository
2. Create a new branch
3. Make your changes
4. Submit a pull request

