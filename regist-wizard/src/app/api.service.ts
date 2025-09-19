import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../enviroments/enviroment';
import { IndustryDto, RegistrationRequest, RegistrationResponse } from './models';
import { LoginRequest, LoginResponse } from './models';

@Injectable({providedIn: 'root'})
export class ApiService{
    private baseUrl = environment.apiBaseUrl;

    constructor(private http: HttpClient){}

   /**
   * Fetches all industries from the API.
   *
   * @returns {Observable<IndustryDto[]>} List of industries.
   */
    getIndustries(): Observable<IndustryDto[]>{

        return this.http.get<IndustryDto[]>(`${this.baseUrl}/industry`);
    }

   /**
   * Checks whether a given username is available.
   *
   * @param username The username to check.
   * @returns {Observable<boolean>} True if available, false otherwise.
   */
    checkUsername(username : string): Observable<boolean>{
        const params = new HttpParams().set('username',username);
        return this.http.get<boolean>(`${this.baseUrl}/user/check-username`, {params});
    }

   /**
   * Registers a new user with the provided data.
   *
   * @param payload Registration details of the user.
   * @returns {Observable<RegistrationResponse>} API response after registration.
   */
    register(payload: RegistrationRequest):Observable<RegistrationResponse>{
        return this.http.post<RegistrationResponse>(`${this.baseUrl}/registration`, payload)
    }
    
    /**
    * Authenticates a user with the provided credentials.
    *
    * @param payload Login details containing username and password.
    * @returns {Observable<LoginResponse>} API response after login attempt.
    */
    login(payload: LoginRequest){
        return this.http.post<LoginResponse>(`${this.baseUrl}/authentication/login`, payload);
    }

}
