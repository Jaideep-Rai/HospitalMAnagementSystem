import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
declare var $: any;
@Component({
  selector: 'app-medicine',
  templateUrl: './medicine.component.html',
  styleUrls: ['./medicine.component.css']
})
export class MedicineComponent implements OnInit {
  medicines: any=[];
  errorMessage: string;
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    //$(document).ready(function () {
    //  $('#dataTable').DataTable();
    //});
    this.get();
  }

  get() {
    this.apiService.getData(`medicinemaster/0`, null)
      .subscribe((res: any) => {
        debugger;
        if (!res) {
          /*alert("Could not load data at this time. Try again later.")*/
        }
        else {
          this.medicines = res;
        }
      }, error => {
        this.errorMessage = "Could not load data at this time. Try again later."
      });
  }

  delete(medicine) {
    this.apiService.deleteData(`medicinemaster/${medicine.id}`, medicine.id).subscribe((res: any) => {
      this.get();
      if (res) {
        alert("Data deleted");
      }
      else {
        this.errorMessage = res; }
    }, error => {
      alert("Error deleting, try again after sometime.");
    })
  }
  
 
}
