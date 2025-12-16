import type { TipSplit } from "../types/TipSplit";
import type { Shift } from "../types/Shift";
import type { Employee } from "../types/Employee";

const BASE_URL = "https://localhost:7043/api";

// Fetch all employees
export async function getEmployees(): Promise<Employee[]> {
  const res = await fetch(`${BASE_URL}/employees`);
  if (!res.ok) throw new Error("Failed to fetch employees");
  return res.json();
}

// Fetch shifts for a week
export async function getShiftsForWeek(weekStart: Date): Promise<Shift[]> {
  const res = await fetch(`${BASE_URL}/shifts/week?weekStart=${weekStart.toISOString()}`);
  if (!res.ok) throw new Error("Failed to fetch shifts");
  return res.json();
}

// Add a new shift
export async function addShift(shift: {
  employeeId: string;
  date: string;      
  startTime: string;  
  endTime: string;   }) 
{
  const payload = {
    EmployeeId: shift.employeeId,
    Date: shift.date,                 
    StartTime: shift.startTime,      
    EndTime: shift.endTime
  };

  const res = await fetch(`${BASE_URL}/shifts`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (!res.ok) throw new Error("Failed to add shift");
  return res.json();
}

// Get the weekly tip split
export async function getWeeklyTips(): Promise<TipSplit> {
  const today = new Date();
  const weekStart = new Date(today);
  weekStart.setDate(today.getDate() - today.getDay() + 1); // Monday

  const res = await fetch(
    `https://localhost:7043/api/tips/weekly-split?weekStart=${weekStart.toISOString()}`
  );
  if (!res.ok) throw new Error("Failed to fetch weekly tips");
  return res.json() as Promise<TipSplit>;
}

// Add a new tip
export async function addTip(tip: { date: string; amount: number }) {
  const res = await fetch(`${BASE_URL}/tips`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ Date: tip.date, Amount: tip.amount }),
  });

  if (!res.ok) throw new Error("Failed to add tip");

  return true;
}



