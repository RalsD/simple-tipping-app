import { useEffect, useState } from 'react';
import { getWeeklyTips } from '../services/api';
import type { Employee } from '../types/Employee';
import type { TipSplit } from '../types/TipSplit';
import { getEmployees } from '../services/api';

interface TipSplitProps {
  refresh?: boolean; 
}

export default function TipSplit({ refresh }: TipSplitProps) {
  const [tips, setTips] = useState<TipSplit>({});
  const [employees, setEmployees] = useState<Employee[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [tipData, employeeData] = await Promise.all([
          getWeeklyTips(),
          getEmployees(),
        ]);
        setTips(tipData);
        setEmployees(employeeData);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [refresh]);

  if (loading) return <p>Loading weekly tips...</p>;

  return (
    <div>
      <h2>Tip Split</h2>
      <ul>
        {employees.map(emp => (
          <li key={emp.id}>
            {emp.firstName} {emp.lastName}: {tips[emp.id] ?? 0} €
          </li>
        ))}
      </ul>
    </div>
  );
}
