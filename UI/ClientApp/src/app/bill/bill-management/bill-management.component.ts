import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-bill-management',
  templateUrl: './bill-management.component.html',
  styleUrls: ['./bill-management.component.css']
})
export class BillManagementComponent implements OnInit {
bill: any = {}
  constructor() { }

  ngOnInit() {
  }
calculateTotal(event:any){
  this.bill.total = +this.bill.charges + +event.target.value;
}
  diagnosticChangeCharges(event: any) {
    let value =event.target.value
    if (value == "bloodtest") {
      this.bill.charges = 200;
    }
    if (value == "ctscan") {
      this.bill.charges = 3000;
    }
    if (value == "ecg") {
      this.bill.charges = 300;
    }
    if (value == "kft") {
      this.bill.charges = 500;
    }
    if (value == "lft") {
      this.bill.charges = 550;
    }
    if (value == "mri") {
      this.bill.charges = 5000;
    }
    if (value == "ultrasound") {
      this.bill.charges = 600;
    }
    if (value == "xray") {
      this.bill.charges = 350;
    }

  }

}
