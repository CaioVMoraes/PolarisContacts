const sass = require('gulp-sass')(require('sass'));
const { src, dest, watch } = require("gulp");


function generateCSS(cb) {
    src('./wwwroot/sass/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(dest('./wwwroot/css'));
    cb();
}

exports.css = function () {
    watch('./wwwroot/sass/**/*.scss', generateCSS)
};
