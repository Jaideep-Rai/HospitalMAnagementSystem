import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './views/home/home.component';
import { HeaderComponent } from './Component/header/header.component';
import { MedicineComponent } from './views/medicine/medicine.component';
import { SidebarComponent } from './Component/sidebar/sidebar.component';
import { CreateMedecineComponent } from './views/medicine/create-medecine/create-medecine.component';
import { ApiService } from './services/api.service';
import { BillComponent } from './bill/bill.component';
import { BillManagementComponent } from './bill/bill-management/bill-management.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    MedicineComponent,
    SidebarComponent,
    CreateMedecineComponent,
    BillComponent,
    BillManagementComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'medicines', component: MedicineComponent },
      { path: 'medicines/add', component: CreateMedecineComponent },
      { path: 'bill', component: BillComponent },
      { path: 'bill/add', component: BillManagementComponent },
     
    ])
  ],
  providers: [ApiService, AppComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
