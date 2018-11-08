import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICurrency } from '../components/main/i-currency';

@Injectable()
export class CommonService {

    url = ''; // 'http://localhost:60909/';
    constructor(private http: HttpClient) {
    }

    getRate(currencyId: number, date: Date): Observable<string> {
        return this.http.get(`${this.url}api/Rates/${currencyId}/${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`)
        .pipe(
            map((data: string) => {
                return data;
            })
        );
    }

    getCurrencies(): Observable<ICurrency[]> {
        return this.http.get(`${this.url}api/Currencies`)
        .pipe(
            map((data: any) => {
                // console.log(data);
                return <ICurrency[]>data;
            })
        );
    }

    getMinDate(): Observable<Date> {
        return this.http.get(`${this.url}api/MinDate`)
        .pipe(
            map((data: any) => {
                console.log(`data: ${data}`);
                const minDate = new Date(data);
                console.log(`minDate: ${minDate}`);
                return minDate;
            })
        );
    }

    getMaxDate(): Observable<Date> {
        return this.http.get(`${this.url}api/MaxDate`)
        .pipe(
            map((data: any) => {
                console.log(`data: ${data}`);
                const maxDate = new Date(data);
                console.log(`maxDate: ${maxDate}`);
                return new Date(maxDate);
            })
        );
    }
}
