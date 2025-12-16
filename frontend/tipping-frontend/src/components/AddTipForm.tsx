import { useState } from "react";
import { addTip } from "../services/api";

interface AddTipFormProps {
  onTipAdded?: () => void;
}

export default function AddTipForm({ onTipAdded }: AddTipFormProps) {
  const [date, setDate] = useState("");
  const [amount, setAmount] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await addTip({ date, amount: parseFloat(amount) });
      onTipAdded?.();
      setDate("");
      setAmount("");
    } catch (err) {
      console.error(err);
      alert("Failed to add tip");
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ marginTop: "1rem" }}>
      <h3>Add Tip</h3>
      <input
        type="date"
        value={date}
        onChange={(e) => setDate(e.target.value)}
        required
      />
      <input
        type="number"
        step="0.01"
        placeholder="Amount"
        value={amount}
        onChange={(e) => setAmount(e.target.value)}
        required
      />
      <button type="submit">Add Tip</button>
    </form>
  );
}
