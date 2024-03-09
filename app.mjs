// app.mjs

import express from 'express';

const app = express();
const port = 3000;

// Serve static files from the public directory
app.use('/public', express.static('public'));

// Define a simple array of products
const products = [
  { id: 1, name: 'iPhone 15 pro max', price: 1119.99, image: 'iphone.jpg' },
  { id: 2, name: 'Samsung galaxy s22 Ultra', price: 1029.99, image: 'SAMSUNG.jpg' },
  {id:3, name:"Electric trimmer", price:50, image:'razor.jpg'}
  // Add more products as needed
];

// Set the view engine to EJS
app.set('view engine', 'ejs');

// Define a route for the homepage
app.get('/', (req, res) => {
  res.render('pages/home', { title: "Home Page", products: products });
});
app.get('/account', (req, res)=> {
  res.render('pages/account', {title: "Account Page"});
});
app.get('/signup', (req,res )=> {
  res.render('pages/signup', {title: "SignUp Page"});
});
app.get('/search', (req, res) => {
  res.render('pages/search', {title: "Search page"});
});
app.get('/admin', (req, res)=> {
  res.render('pages/admin', {title: "Admin page"});
});
app.get('/admin/add-product', (req, res)=> {
  res.render('pages/add-product', {title: "Add product page"});
});


// Start the server
app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
});
