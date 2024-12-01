import { Label } from "./Label";

export interface Task {       /* coloquei ? provisoriamente, porem só vai em dados que não seja obrigatorios, por ex: descrição*/
    taskId?: number;
    name: string;
    description?: string;
    isCompleted?: boolean;
    createdAt?: string;
    userId: number;
    labelId?: number;
    labels?: Label[];
  }