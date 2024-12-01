import { Task } from "./Task";

export interface Label {
    labelId?: number;
    labelName: string;
    userId?: number;
    tasks?: Task[];
  }