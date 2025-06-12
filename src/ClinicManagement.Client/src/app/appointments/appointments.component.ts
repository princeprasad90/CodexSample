import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppointmentService, Appointment } from './appointment.service';

@Component({
  selector: 'app-appointments',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './appointments.component.html'
})
export class AppointmentsComponent implements OnInit {
  appointments: Appointment[] = [];

  form = this.fb.nonNullable.group({
    patientId: [0, Validators.required],
    date: ['', Validators.required],
    description: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private service: AppointmentService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.service.getAppointments().subscribe(a => (this.appointments = a));
  }

  add(): void {
    if (this.form.invalid) return;
    this.service.addAppointment(this.form.getRawValue()).subscribe(() => {
      this.form.reset();
      this.load();
    });
  }
}
