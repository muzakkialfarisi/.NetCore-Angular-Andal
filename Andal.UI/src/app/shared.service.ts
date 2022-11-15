import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class SharedService {
  
  readonly APIUrl = "https://localhost:44328/api";

  constructor(private http:HttpClient) { }

  getJobTitleList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/jobtitles')
  }

  addJobTitle(val:any){
    return this.http.post(this.APIUrl+'/jobtitles', val)
  }

  updateJobTitle(val:any){
    return this.http.put(this.APIUrl+'/jobtitles', val)
  }

  deleteJobTitle(val:any){
    return this.http.delete(this.APIUrl+'/jobtitles/'+val);
  }

  getJobPositionList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/jobpositions')
  }

  addJobPosition(val:any){
    return this.http.post(this.APIUrl+'/jobpositions', val)
  }

  updateJobPosition(val:any){
    return this.http.put(this.APIUrl+'/jobpositions', val)
  }

  deleteJobPosition(val:any){
    return this.http.delete(this.APIUrl+'/jobpositions/'+val)
  }
}
