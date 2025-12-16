import { useEffect, useState } from "react";
import type { Shift } from "../types/Shift";
import type { Employee } from "../types/Employee";
import { getShiftsForWeek } from "../services/api";

interface WeeklyShiftsProps {
  employees: Employee[];
  shiftsUpdated?: boolean;
}

export default function WeeklyShifts({ employees, shiftsUpdated }: WeeklyShiftsProps) {
  const [shifts, setShifts] = useState<Shift[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchShifts = async () => {
      try {
        setLoading(true);
        const today = new Date();
        const weekStart = new Date(today);
        weekStart.setDate(today.getDate() - today.getDay() + 1);

        const data = await getShiftsForWeek(weekStart);
        setShifts(data);
      } catch (err) {
        console.error(err);
        setError("Failed to fetch shifts");
      } finally {
        setLoading(false);
      }
    };

    fetchShifts();
  }, [shiftsUpdated]);

  if (loading) return <p>Loading shifts...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h2>Weekly Shifts</h2>
      <table>
        <thead>
          <tr>
            <th>Employee</th>
            <th>Date</th>
            <th>Start</th>
            <th>End</th>
            <th>Hours</th>
          </tr>
        </thead>
        <tbody>
          {shifts.map((s) => {
            const emp = employees.find((e) => e.id === s.employeeId);
            return (
              <tr key={s.id}>
                <td>{emp ? `${emp.firstName} ${emp.lastName}` : s.employeeId}</td>
                <td>{new Date(s.date).toLocaleDateString()}</td>
                <td>{s.startTime}</td>
                <td>{s.endTime}</td>
                <td>{s.hoursWorked?.toFixed(2)}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}
