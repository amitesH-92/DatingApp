import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private http: HttpClient, private accountService: AccountService) { }
  ngOnInit(): void {
    this.getUser();
    this.setCurrentUser();
  }
  getUser() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: () => console.log(Error),
      complete: () => console.log('request is completed')

    });
  }
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user')!)
    this.accountService.setCurrentUser(user);
  }

  users: any;
  title = 'Dating App';
}
