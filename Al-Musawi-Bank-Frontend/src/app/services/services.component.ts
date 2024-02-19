import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  styleUrls: ['./services.component.css']
})
export class ServicesComponent implements OnInit, AfterViewInit {
  @ViewChild('investmentChart') investmentChart!: ElementRef<HTMLCanvasElement>;
  chart!: Chart;

  constructor() {
    this.chart = {} as Chart;
  }

  ngOnInit(): void {
    // OnInit logic here
  }

  ngAfterViewInit(): void {
    this.initChart();
  }

  initChart(): void {
    this.chart = new Chart(this.investmentChart.nativeElement, {
      type: 'line',
      data: {
        labels: ['Year 1', 'Year 2', 'Year 3', 'Year 4', 'Year 5', 'Year 6', 'Year 7', 'Year 8', 'Year 9', 'Year 10'], 
        datasets: [{
          label: 'Investment Growth',
          data: [10000, 11500, 13250, 15200, 17500, 20000, 23000, 26450, 30418, 34990],
          fill: false,
          borderColor: 'rgb(75, 192, 192)',
          tension: 0.1
        }]
      }
    });
  }
}
