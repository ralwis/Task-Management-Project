import { Component, Inject, inject, model, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { TaskDetailService } from '../../services/task-detail.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { Status } from '../../../register/models/status.model';
import { Task } from '../../models/task.model';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule
  ],
  templateUrl: './task-detail.component.html',
  styleUrl: './task-detail.component.css'
})
export class TaskDetailComponent extends BaseComponent implements OnInit{
  private formBuilder = inject(FormBuilder);
  private detailService = inject(TaskDetailService);

  taskForm!: FormGroup;
  statusOptions = model<Status[]>();
  isCreation = model<boolean>(false);
  existingTask = model<Task>();

  constructor(
    public dialogRef: MatDialogRef<TaskDetailComponent>,
  ) {
    super();
  }

  ngOnInit(): void {
    this.taskForm = this.formBuilder.group({
      id: [null],
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      dueDate: [''],
      statusId: [1, Validators.required]
    });

    if(!this.isCreation()){
      const task = this.existingTask();

      if (task) {
        this.taskForm.patchValue({
          title: task.title,
          description: task.description || '',
          dueDate: task.dueDate ? new Date(task.dueDate) : '',
          statusId: task.statusId
        });
      }
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.taskForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';

      const taskData : Task = {
        id: this.isCreation() ? 0 : this.existingTask()?.id ?? 0,
        title: this.taskForm.value.title,
        description: this.taskForm.value.description || undefined,
        dueDate: this.taskForm.value.dueDate ? new Date(this.taskForm.value.dueDate).toISOString() : undefined,
        statusId: this.taskForm.value.statusId,
        completedDate: this.existingTask()?.completedDate ?? undefined,
        createdDate: this.existingTask()?.createdDate ?? undefined
      };

      if(this.isCreation()){
        this.detailService.createTask(taskData).subscribe({
          next: (response) => {
            this.isLoading = false;
            this.dialogRef.close(response);
          },
          error: (error) => {
            this.isLoading = false;
            this.errorMessage = error.message || 'Failed to create task. Please try again.';
          }
        });
      }
      else{
        this.detailService.updateTask(taskData).subscribe({
          next: (response) => {
            this.isLoading = false;
            this.dialogRef.close(response);
          },
          error: (error) => {
            this.isLoading = false;
            this.errorMessage = error.message || 'Update failed. Please try again.';
          }
        });
      }


    } else {
      Object.keys(this.taskForm.controls).forEach(key => {
        this.taskForm.get(key)?.markAsTouched();
      });
    }
  }
}
