export interface Task {
  id: number;
  title: string;
  description?: string;
  createdDate?: string;
  dueDate?: string;
  completedDate?: string;
  statusId: number;
  statusDisplayName?: string;
}
