# simple-tipping-app

Simple Tipping & Roster App

A small web application for managing employee shifts and weekly tip distribution.
Built with ASP.NET Core Web API for the backend and React + TypeScript for the frontend.

Frontend

	Employee list – view all employees with total hours worked.

	Weekly shifts – view all shifts for the current week.

	Add Shift – create new shifts for employees; updates weekly shifts table automatically.

	Weekly tip split – displays calculated tip amounts per employee based on their hours worked.

	Add Tip – add weekly tips; recalculates weekly split automatically.

Backend

	ShiftsController – endpoints to create shifts, get shifts by ID, or fetch weekly shifts.

	TipsController – endpoints to add tips, get tip by ID, and calculate weekly tip split.

	CQRS + MediatR – all actions handled via commands and queries.

	Unit tests – basic unit tests for core services and commands.

Getting Started/Prerequisites

	.NET 8 SDK

	Node.js 20+
	+ npm or yarn

Backend

	Navigate to the backend project folder:

	cd backend/TippingApi


Restore dependencies and build:

	dotnet restore
	dotnet build


Run the API:

	dotnet run


The API will be available at https://localhost:7043/api.

Frontend

	Navigate to the frontend folder:

	cd frontend/tipping-frontend


Install dependencies:

	npm install
	# or
	yarn


Start the development server:

	npm start
	# or
	yarn start


Open http://localhost:5173 in your browser to view the app.


Project Structure

frontend/
  ├─ src/
  │   ├─ components/      # React components (EmployeeList, WeeklyShifts, TipSplit, AddShiftForm)
  │   ├─ services/        # API calls and type definitions
  │   ├─ types/           # TypeScript types (Employee, Shift, TipSplit)
  │   └─ App.tsx          # Main component
  
  
backend/
  ├─ TippingApi/
  │   ├─ Controllers/     # API controllers
  │   ├─ Application/     # Commands, Queries, Handlers
  │   ├─ Domain/          # Entities and Aggregates
  │   └─ Program.cs       # Entry point



Usage

	Add employees via backend database (or use preloaded seed data).

	Add shifts for employees via Add Shift form.

	Add weekly tips via Add Tip form.

	Observe updated Weekly Shifts and Weekly Tip Split tables automatically.