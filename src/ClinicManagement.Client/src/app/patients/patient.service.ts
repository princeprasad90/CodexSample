import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Patient {
  id: number;
  name: string;
  dateOfBirth: string;
}

@Injectable({ providedIn: 'root' })
export class PatientService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(`${this.baseUrl}/patients`);
  }

  addPatient(patient: Omit<Patient, 'id'>): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/patients`, patient);
  }
}
