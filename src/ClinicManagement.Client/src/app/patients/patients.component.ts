import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PatientService, Patient } from './patient.service';

@Component({
  selector: 'app-patients',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './patients.component.html'
})
export class PatientsComponent implements OnInit {
  patients: Patient[] = [];
  form = this.fb.nonNullable.group({
    name: ['', Validators.required],
    dateOfBirth: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private service: PatientService) {}

  ngOnInit(): void {
    this.load();
  }

  load(): void {
    this.service.getPatients().subscribe(p => (this.patients = p));
  }

  add(): void {
    if (this.form.invalid) return;
    this.service.addPatient(this.form.getRawValue()).subscribe(() => {
      this.form.reset();
      this.load();
    });
  }
}
