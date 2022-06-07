using AspNedelja3.Implementation.Validators;
using ASPNedelja3.Application.UseCases;
using ASPNedelja3.Application.UseCases.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNedelja3.Implementation.UseCases.Queries.SP
{
    public class GetUseCaseLogsQuery : IGetUseCaseLogsQuery
    {
        public int Id => 10;

        public string Name => "Search use case logs";

        public string Description => "";

        private IUseCaseLogger _logger;
        private SearchUseCaseLogsValidator _validator;

        public GetUseCaseLogsQuery(IUseCaseLogger logger, SearchUseCaseLogsValidator validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public IEnumerable<UseCaseLog> Execute(UseCaseLogSearch search)
        {
            _validator.ValidateAndThrow(search);
            return _logger.GetLogs(search);
        }
    }
}
