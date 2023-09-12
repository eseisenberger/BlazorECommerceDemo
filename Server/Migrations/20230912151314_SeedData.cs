using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorECommerceDemo.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "The Lord of the Rings is an epic[1] high-fantasy novel[a] by the English author and scholar J. R. R. Tolkien. Set in Middle-earth, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work. Written in stages between 1937 and 1949, The Lord of the Rings is one of the best-selling books ever written, with over 150 million copies sold.[2]", "https://upload.wikimedia.org/wikipedia/en/e/e9/First_Single_Volume_Edition_of_The_Lord_of_the_Rings.gif", 9.99m, "The Lord of the Rings" },
                    { 2, "A Song of Ice and Fire is a series of epic fantasy novels by the American novelist and screenwriter George R. R. Martin. He began writing the first volume, A Game of Thrones in 1991, publishing it in 1996. Martin originally envisioned the series as a trilogy but as of 2023 has released five out of a planned seven volumes. The fifth and most recent entry in the series, A Dance with Dragons, was published in 2011. As of 2023 Martin continues to write the sixth novel, titled The Winds of Winter.[3] A seventh novel, A Dream of Spring, is planned to follow.[4]", "https://upload.wikimedia.org/wikipedia/en/d/dc/A_Song_of_Ice_and_Fire_book_collection_box_set_cover.jpg", 7.99m, "A Song of Ice and Fire" },
                    { 3, "The Earthsea Cycle, also known as simply, Earthsea, is a series of high fantasy books written by the American writer Ursula K. Le Guin. Beginning with A Wizard of Earthsea (1968), The Tombs of Atuan, (1970) and The Farthest Shore (1972), the series was continued in Tehanu (1990), and Tales from Earthsea and The Other Wind (both 2001). In 2018, all the novels and short stories were published in a single volume, The Books of Earthsea: The Complete Illustrated Edition, with artwork by Charles Vess.", "https://upload.wikimedia.org/wikipedia/en/7/7f/The_Books_of_Earthsea.jpg", 11.99m, "Earthsea" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
