import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-log-in-page',
  templateUrl: './log-in-page.component.html',
  styleUrls: ['./log-in-page.component.css']
})
export class LogInPageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  login(userName,password) {
    debugger
    if (userName == 'admin' && password == 'Admin123') {
      this.router.navigate(['/hrms'])
    }
    else {
      alert('Username and Password incorrect.')
    }
  }
}
