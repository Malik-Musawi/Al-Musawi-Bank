import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  testimonials: any[] = [];
  latestNews: any[] = [];
  showTestimonialForm: boolean = false; 
  newTestimonial: { author: string; text: string; rating: number } = {
    author: '',
    text: '',
    rating: 0,
  };

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.loadTestimonials();
    this.loadLatestNews();
  }

  loadTestimonials(): void {
    // This data could be fetched from a backend service
    this.testimonials = [
      { author: 'John Doe', text: 'I love how easy it is to manage my finances with Al-Musawi Bank!', rating: 4 },
      { author: 'Jane Smith', text: 'The customer service at Al-Musawi Bank is top-notch!', rating: 5 },
      // ... more testimonials
    ];
  }

  loadLatestNews(): void {
    // This data could be fetched from a backend service
    this.latestNews = [
      { title: 'New Branch Opening', content: 'We are excited to announce a new branch opening in downtown.' },
      // ... more news items
    ];
  }




  addTestimonial(): void {
    if (this.newTestimonial.author && this.newTestimonial.text && this.newTestimonial.rating > 0) {
      this.testimonials.unshift(this.newTestimonial); // Add to the beginning of the array
      this.newTestimonial = { author: '', text: '', rating: 0 }; // Reset for next entry
    }
  }

  setRating(rating: number): void {
    this.newTestimonial.rating = rating;
  }

  onServicesButtonClick(): void {
    this.router.navigate(['/services']); // Implement navigation logic
  }

  toggleTestimonialForm(): void {
    this.showTestimonialForm = !this.showTestimonialForm;
  }
  
}
