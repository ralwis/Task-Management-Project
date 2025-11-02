import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { TaskRegisterService } from '../../services/task-register.service';
import { BaseComponent } from '../../../../shared/components/base/base.component';
import { Task } from '../../../detail/models/task.model';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Status } from '../../models/status.model';
import { forkJoin } from 'rxjs';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TaskDetailComponent } from '../../../detail/components/task-detail/task-detail.component';

@Component({
  selector: 'app-task-register',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule
  ],
  templateUrl: './task-register.component.html',
  styleUrl: './task-register.component.css'
})
export class TaskRegisterComponent extends BaseComponent implements OnInit{
  private registerService = inject(TaskRegisterService);
  private dialog = inject(MatDialog);

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns: string[] = ['title', 'description', 'statusId', 'actions'];
  dataSource = new MatTableDataSource<Task>();

  isPopupOpen: boolean = false;
  statuses!: Status[];

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.isLoading = true;

    forkJoin({
      tasks: this.registerService.getAllTasks(),
      statuses: this.registerService.getStatuses()
    }).subscribe({
      next: (response) => {
        this.isLoading = false;

        this.statuses = response.statuses;
        const tasksWithStatusDisplay = response.tasks.map(task => {
          const status = response.statuses.find(s => s.id === task.statusId);
          return {
            ...task,
            statusDisplayName: status ? status.displayName : 'Unknown'
          } as Task;
        });

        this.dataSource.data = tasksWithStatusDisplay;
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        });
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.message;
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDialog(isCreation: boolean, task?: Task): void {
    this.isPopupOpen = true;
    const dialogRef = this.dialog.open(TaskDetailComponent, {
      width: '500px',
      data: {
        title: 'Create New Task',
        statuses: this.statuses
      }
    });

    dialogRef.componentInstance.statusOptions.set(this.statuses);
    dialogRef.componentInstance.isCreation.set(isCreation);
    if(!isCreation){
      dialogRef.componentInstance.existingTask.set(task);
    }

    dialogRef.afterClosed().subscribe((result) => {
    this.isPopupOpen = false;
    if (result) {
      this.loadData();
    }
  });
  }

  onDelete(task: Task) {
    this.registerService.deleteTask(task.id).subscribe({
      next: (response) => {
        if (response) {
          this.loadData();
        } else {
          this.errorMessage = 'Record deletion failed';
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = error.message;
      }
    });
  }

}
