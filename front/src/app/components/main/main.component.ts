import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSelectChange } from '@angular/material';
import { ICurrency } from './i-currency';
import { CommonService } from '../../services/common.service';
import { NgForm, FormsModule, FormControl, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  /**Ошибки */
  error = '';
  /**Курс */
  exchangeRate = '';
  /**Дата */
  exchangeDate: Date = undefined;
  /**Выбранная валюта */
  currency: ICurrency;
  /**Индекс валюты */
  currencyId: number;
  /**Доступные валюты */
  currencies: ICurrency[] = [];
  /**Форма*/
  @ViewChild('currencyRateForm')
  form: NgForm;
  receiptDate: FormControl;
  maxReceiptDate: Date;
  minReceiptDate: Date;

  constructor(public commonService: CommonService) {
    this.maxReceiptDate = new Date();
    this.minReceiptDate = new Date();
    this.receiptDate = new FormControl({disabled: true}, Validators.required);
  }

  ngOnInit(): void {
    forkJoin(
      this.commonService.getCurrencies(),
      this.commonService.getMinDate(),
      this.commonService.getMaxDate(),
    )
    .subscribe((data) => {
      if (data && data.length === 3 && data[0].length) {
        this.minReceiptDate = data[1];
        this.maxReceiptDate = data[2];
        this.currencies.push(...data[0]);
        this.currency = this.currencies[0];
        this.currencyId = this.currency.Id;
        // console.log(this.currencies);
      }
    });

  }

  onChangeCurrency(selected: MatSelectChange) {
    this.currencyId = selected.value;
    this.currency = this.currencies.find(_ => _.Id === this.currencyId);
  }

  onChangeDate(event: any) {
    this.exchangeRate = '';
    this.error = '';
    this.exchangeDate = event.value;
  }

  onSubmitComponent() {
    this.exchangeRate = '';
    this.error = '';
    this.commonService.getRate(this.currencyId, this.exchangeDate)
    .subscribe((rate) => {
        this.exchangeRate = rate;
    },
    (error) => {
      this.error = 'Не удалось получить данные.';
    });
  }

  getExchangeDate() {
    if (this.exchangeDate) {
      const res = `${this.exchangeDate.getFullYear()}-${this.exchangeDate.getMonth() + 1}-${this.exchangeDate.getDate()}`;
      return res;
    } else {
      return '';
    }
  }
}
