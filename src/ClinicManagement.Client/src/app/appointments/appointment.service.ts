import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Appointment {
  id: number;
  patientId: number;
  date: string;
  description: string;
}

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAppointments(patientId?: number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.baseUrl}/appointments`, { params: patientId ? { patientId } : {} });
  }

  addAppointment(appointment: Omit<Appointment, 'id'>): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/appointments`, appointment);
  }
}
