import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { HttpClient } from '@angular/common/http';
import { MatSelectChange } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { environment } from '../../../environments/environment';
import { Country } from '../../models/country.model';
import { Province } from '../../models/province.model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-user-registration',
  standalone: true,
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatOptionModule,
    MatSelectModule,
    MatSnackBarModule
  ]
})
export class UserRegistrationComponent {
  isLinear = true;
  private apiBase = environment.apiUrl;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  @ViewChild(MatStepper) stepper!: MatStepper;

  countries: Country[] = [];
  provinces: Province[] = [];
  serverErrorMessage: string | null = null;

  constructor(private _formBuilder: FormBuilder, private http: HttpClient, private snackBar: MatSnackBar) {
    this.firstFormGroup = this._formBuilder.group(
      {
        login: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d).+$/)]],
        confirmPassword: ['', Validators.required],
        agree: [false, Validators.requiredTrue]
      },
      { validators: this.passwordsMatchValidator }
    );

    this.secondFormGroup = this._formBuilder.group({
      country: ['', Validators.required],
      province: ['', Validators.required]
    });

    this.loadCountries();
  }

  loadCountries() {
    this.http.get<Country[]>(`${this.apiBase}/country`)
      .subscribe(data => this.countries = data);
  }

  onCountryChange(event: MatSelectChange) {
    const countryId = event.value;
    this.secondFormGroup.get('province')?.reset();
    this.provinces = [];

    this.http.get<Province[]>(`${this.apiBase}/province?countryId=${countryId}`)
      .subscribe(data => this.provinces = data);
  }

  onSave() {
    this.serverErrorMessage = null;
    if (this.firstFormGroup.valid && this.secondFormGroup.valid) {
      const registerDto = {
        email: this.firstFormGroup.get('login')?.value,
        password: this.firstFormGroup.get('password')?.value,
        confirmPassword: this.firstFormGroup.get('confirmPassword')?.value,
        agreed: this.firstFormGroup.get('agree')?.value,
        provinceId: this.secondFormGroup.get('province')?.value
      };

      this.http.post(`${this.apiBase}/Auth/register`, registerDto, { responseType: 'text' })
        .subscribe({
          next: (message) => {
            this.snackBar.open('✅ ' + message, 'Close', {
              duration: 4000,
              panelClass: ['snackbar-success']
            });

            this.firstFormGroup.reset();
            this.secondFormGroup.reset();
            this.provinces = [];
            this.stepper.reset();
          },
          error: err => {
            const msg = typeof err.error === 'string' ? err.error : 'Unexpected error occurred.';
            this.snackBar.open('❌ ' + msg, 'Close', {
              duration: 5000,
              panelClass: ['snackbar-error']
            });
          }
        });
    } else {
      this.firstFormGroup.markAllAsTouched();
      this.secondFormGroup.markAllAsTouched();
    }
  }

  private passwordsMatchValidator(group: AbstractControl) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { passwordMismatch: true };
  }
}
