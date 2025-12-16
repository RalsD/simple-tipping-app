import { useState, useEffect } from "react";
import EmployeesList from "./components/EmployeesList";
import TipSplit from "./components/TipSplit";
import WeeklyShifts from "./components/WeeklyShifts";
import AddShiftForm from "./components/AddShiftForm";
import AddTipForm from "./components/AddTipForm";
import type { Employee } from "./types/Employee";
import { getEmployees } from "./services/api";

export default function App() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [shiftsUpdated, setShiftsUpdated] = useState<boolean>(false);
  const [tipsUpdated, setTipsUpdated] = useState<boolean>(false);

  // Load employees on mount
  useEffect(() => {
    getEmployees().then(setEmployees);
  }, []);

  const refreshShifts = () => setShiftsUpdated(prev => !prev);
  const refreshTips = () => setTipsUpdated(prev => !prev);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Simple Tipping & Roster App</h1>

      <EmployeesList />

      <AddShiftForm employees={employees} onShiftAdded={refreshShifts} />

      <WeeklyShifts employees={employees} shiftsUpdated={shiftsUpdated} />

      <AddTipForm onTipAdded={refreshTips} />

      <TipSplit refresh={tipsUpdated} />
    </div>
  );
}
