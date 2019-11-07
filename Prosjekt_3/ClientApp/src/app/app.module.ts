

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { spa } from './SPA';
import { from } from 'rxjs';

@NgModule({
   imports: [BrowserModule, ReactiveFormsModule, HttpClientModule],
    declarations: [spa],
    bootstrap: [spa]


}
)
export class AppModule { }
