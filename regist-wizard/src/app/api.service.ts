import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../enviroments/enviroment';
import { IndustryDto, RegistrationRequest, RegistrationResponse } from './models';

@Injectable({providedIn: 'root'})
export class ApiService{
    private baseUrl = environment.apiBaseUrl;

    constructor(private http: HttpClient){}

    getIndustries(): Observable<IndustryDto[]>{

        return this.http.get<IndustryDto[]>(`${this.baseUrl}/industry`);
    }

    checkUsername(username : string): Observable<boolean>{
        const params = new HttpParams().set('username',username);
        return this.http.get<boolean>(`${this.baseUrl}/user/check-username`, {params});
    }

    register(payload: RegistrationRequest):Observable<RegistrationResponse>{
        return this.http.post<RegistrationResponse>(`${this.baseUrl}/registration`, payload)
    }
    
}
