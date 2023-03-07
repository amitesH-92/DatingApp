import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
registermode=false;
users:any;
constructor(private http:HttpClient){
  this.getUser();
}
ngOnInit(): void {
  
}

registerToggle(){

  this.registermode=!this.registermode
}
getUser() {
  this.http.get('https://localhost:5001/api/users').subscribe({
    next: response => this.users = response,
    error: () => console.log(Error),
    complete: () => console.log('request is completed')

  });
}
cancerRegisterMode(event:boolean){

  this.registermode=event;
}
}
