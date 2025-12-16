import { useState } from "react";
import { addShift } from "../services/api";
import type { Employee } from "../types/Employee";

interface AddShiftFormProps {
  employees: Employee[];
  onShiftAdded?: () => void;
}

export default function AddShiftForm({ employees, onShiftAdded }: AddShiftFormProps) {
  const [employeeId, setEmployeeId] = useState<string>("");
  const [date, setDate] = useState("");
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!employeeId) {
      alert("Please select an employee");
      return;
    }

    try {
      // Ensure time has seconds for backend TimeSpan
      const formatTime = (t: string) => (t.length === 5 ? t + ":00" : t);

      await addShift({
        employeeId,
        date,
        startTime: formatTime(startTime),
        endTime: formatTime(endTime),
      });

      // Reset form
      setEmployeeId("");
      setDate("");
      setStartTime("");
      setEndTime("");

      // Refresh weekly shifts
      onShiftAdded?.();
    } catch (err) {
      console.error(err);
      alert("Failed to add shift");
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginTop: "1rem" }}>
      <select
        value={employeeId}
        onChange={(e) => setEmployeeId(e.target.value)}
        required
      >
        <option value="">Select Employee</option>
        {employees.map((e) => (
          <option key={e.id} value={e.id}>
            {e.firstName} {e.lastName}
          </option>
        ))}
      </select>

      <input
        type="date"
        value={date}
        onChange={(e) => setDate(e.target.value)}
        required
      />
      <input
        type="time"
        value={startTime}
        onChange={(e) => setStartTime(e.target.value)}
        required
      />
      <input
        type="time"
        value={endTime}
        onChange={(e) => setEndTime(e.target.value)}
        required
      />
      <button type="submit">Add Shift</button>
    </form>
  );
}
