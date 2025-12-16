import type { Shift } from "./Shift";

export interface Employee {
  id: string;
  firstName: string;
  lastName: string;
  shifts: Shift[];
  hoursWorked?: number;
}
