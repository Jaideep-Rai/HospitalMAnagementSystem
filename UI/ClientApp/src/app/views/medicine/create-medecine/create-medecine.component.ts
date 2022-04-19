import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-create-medecine',
  templateUrl: './create-medecine.component.html',
  styleUrls: ['./create-medecine.component.css']
})
export class CreateMedecineComponent implements OnInit {
  medicine: any = {};
  constructor(private apiService: ApiService, private router: Router) { }

  ngOnInit() {
  }
  save() {
    this.apiService.postData('MedicineMaster', this.medicine).subscribe(res => {
      this.router.navigate(['/medicines']);
    })
  }
}
