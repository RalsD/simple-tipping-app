import { useEffect, useState } from "react";
import type { Employee } from "../types/Employee";
import type { Shift } from "../types/Shift";
import { getEmployees } from "../services/api";


export default function EmployeesList() {
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getEmployees()
      .then(setEmployees)
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <p>Loading employees...</p>;

  return (
    <div>
      <h2>Employees</h2>
      <ul>
        {employees.map((e) => (
          <li key={e.id}>
            {e.firstName} {e.lastName} -{" "}
            {e.shifts.reduce((acc: number, s: Shift) => acc + s.hoursWorked, 0)} hrs
          </li>
        ))}
      </ul>
    </div>
  );
}
